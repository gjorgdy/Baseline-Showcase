
window.addEventListener("resize", event => {
    calculateColumnCount();
});

window.addEventListener("DOMContentLoaded", async event => {
    calculateColumnCount();
    await fillGrid();
});

function getUrlUserId() {
    const currentPath = window.location.pathname;
    const [, , userId] = currentPath.split("/");
    return parseInt(userId, 10);
}

async function fillGrid() {
    document.title = "Loading... - Baseline";
    
    let json = await getProfileRequest(getUrlUserId());
    window.editMode = json["isLoggedInUser"];

    document.title = json["user"]["displayName"] + " - Baseline";
    
    const grid = document.createElement("div");
    grid.id = "grid";
    const orderedTiles = orderTiles(json["tiles"]);
    
// Profile information
    // Profile Picture
    let pfpTile = createTileBase(1, 1, null, false);
    pfpTile = appendContent(
        pfpTile,
        `<img src="${json["user"]["profilePictureUri"]}" alt="Profile Picture">`,
        "profile-picture",
    )
    grid.appendChild(pfpTile);
    // Username
    let usernameTile = createTileBase(2, 1, null, false);
    usernameTile = appendContent(
        usernameTile,
        `<div class="center"><h1>${json["user"]["displayName"]}</h1></div>`,
        "username"
    )
    grid.appendChild(usernameTile);
// Profile Content
    orderedTiles.forEach((tile) => {
        let tileElement = createTileBase(tile["width"], tile["height"], tile["id"]);
        tileElement = appendTileContent(tileElement, tile);
        grid.appendChild(tileElement);
    })
// Add tile
    if (window.editMode) {
        let tileElement = createTileBase(1, 1, null, false);
        tileElement = appendContent(
            tileElement,
            `<button onclick="openModal()"><div class="button">＋</div></button>`,
            "add"
        );
        grid.appendChild(tileElement);
    }
// Fill the html
    let container = document.getElementById("grids");
    container.innerHTML = "";
    container.appendChild(grid);
// Setup sortable dragging if relevant 
    setupSortable();
}

function updateTileContent(tileId) {
    let tileData = getTileRequest(getUrlUserId(), tileId);
    let tileElement = document.getElementById(tileId);
    tileElement.innerHTML = "";
    tileElement = updateTileBase(tileElement, tileData["width"], tileData["height"], tileData["id"])
    tileElement = appendTileContent(tileElement, tileData);
}

function appendTileContent(tileElement, tile) {
    let content;
    if (tile["type"] === "paragraph") {
        content = Handlebars.templates.ParagraphTemplate(tile["attributes"]);
    } else if (tile["type"] === "skills") {
        content = Handlebars.templates.SkillsTemplate(tile["attributes"])
    }
    return appendContent(tileElement, content.replace(/[\u200B-\u200D\uFEFF]/g, ""), tile["type"]);
}

function orderTiles(tiles) {
    let tail = tiles.filter(tile => tile["nextTileId"] === null)[0];
    if (tail == null) return [];
    let sortedTiles = [tail];
    
    let current = tail;
    while (true) {
        let predecessor = null;
        for (const obj of tiles) {
            if (obj["nextTileId"] === current["id"]) {
                predecessor = obj;
                break;
            }
        }
        if (!predecessor) break;
        sortedTiles.unshift(predecessor); // Add to the beginning of the list
        current = predecessor;
    }

    return sortedTiles;
}

function createTileBase(width, height, id=null, draggable=true) {
    let tileElement = document.createElement("div");
    return updateTileBase(tileElement, width, height, id, draggable)
}

function updateTileBase(tileElement, width, height, id=null, draggable=true) {
    if (!draggable) {
        tileElement.classList.add("not-draggable");
    }
    if (id != null) {
        tileElement.id = id;
    }
    tileElement.classList.add("tile");
    tileElement.style.height = `calc(${height} * var(--cell-size))`;
    tileElement.style.width = `calc(${width} * var(--cell-size))`;
    tileElement.style.aspectRatio = String(width / height);
    tileElement.style.gridArea = `span ${height} / span ${width}`
    tileElement.tabIndex = 1;
    if (window.editMode && draggable) {
        let editButton = document.createElement("button");
        editButton.classList.add("edit-tile");
        editButton.classList.add("not-draggable");
        editButton.textContent = "Edit";
        editButton.role = "button";
        tileElement.appendChild(editButton);
        editButton.onclick = async (ev) => {
            await openModal(ev, id);
        };
    }
    return tileElement;
}

function appendContent(tile, content, type) {
    let tileContent = document.createElement("div");
    tileContent.classList.add("tile-content")
    tileContent.classList.add(type);
    tileContent.innerHTML = content;
    tile.appendChild(tileContent);
    return tile;
}

function calculateColumnCount() {
    const cellSize = 200; // Example of cell size in pixels
    const container = document.getElementById("container")
    let columnCount = Math.floor(container.offsetWidth / cellSize);
    columnCount = columnCount - (columnCount % 3);
    container.style.setProperty('--column-count', String(columnCount));
}

function setupSortable() {
    if (!window.editMode) return;
    
    const grid = document.getElementById("grid")
    new Sortable(grid, {
        filter: ".not-draggable",
        group: "grids",
        animation: 150,

        onMove: function (evt, originalEvent) {
            // Don't swap if not-draggable tile
            if (evt.related.classList.contains("not-draggable")) {
                return false;
            }
        },

        onEnd: async function (evt) {
            if (evt.oldIndex === evt.newIndex) return;

            let tileOrder = []
            for (let key in evt.to.children) {
                let tile = evt.to.children[key];
                if (tile.id != null && tile.id !== "") {
                    tileOrder.push(tile.id);
                }
            }

            await reorderTilesRequest(getUrlUserId(), tileOrder);
        }
    });
}
