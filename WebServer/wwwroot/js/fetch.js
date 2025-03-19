
window.onload = getData;

async function getData() {
    const url = "https://localhost:7052/users/2/profile";
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const json = await response.json();
        console.log(json);
        
        const grid = document.getElementById("grid");
        
    // Profile information
        // Profile Picture
        let pfpTile = createTile(1, 1, "profile-picture", false);
        // pfpTile.innerHTML = Handlebars.templates.ProfilePictureTemplate(json["user"]);
        pfpTile = appendContent(
            pfpTile,
            `<img src="${json["user"]["profilePicture"]}" alt="Profile Picture">`,
            "profile-picture",
        )
        grid.appendChild(pfpTile);
        // Username
        let usernameTile = createTile(2, 1, "username", false);
        usernameTile = appendContent(
            usernameTile,
            `<div class="center"><h1>${json["user"]["displayName"]}</h1></div>`,
            "username"
        )
        grid.appendChild(usernameTile);
    // Profile Content
        json["tiles"].forEach((tile) => {
            let tileElement = createTile(tile["width"], tile["height"], tile["id"]);
            if (tile["type"] === "paragraph") {
                tileElement = appendContent(
                    tileElement,
                    Handlebars.templates.ParagraphTemplate(tile["attributes"]),
                    tile["type"]
                )
            } else {
                tileElement = appendContent(tileElement, tile["attributes"]["type"], tile["type"])
            }
            grid.appendChild(tileElement);
        })
    } catch (error) {
        console.error(error.message);
    }
}

function createTile(width, height, id, draggable=true) {
    let tileElement = document.createElement("div");
    if (!draggable) {
        tileElement.classList.add("not-draggable");
    }
    tileElement.id = id;
    tileElement.classList.add("tile");
    tileElement.style.height = `calc(${height} * var(--cell-size))`;
    tileElement.style.width = `calc(${width} * var(--cell-size))`;
    tileElement.style.aspectRatio = String(width / height);
    tileElement.style.gridArea = `span ${height} / span ${width}`
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
