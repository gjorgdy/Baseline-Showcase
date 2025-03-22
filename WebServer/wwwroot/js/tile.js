Handlebars.registerHelper('select', function (value, options) {
    return options.fn()
        .split('\n')
        .map(function (v) {
            var t = 'value="' + value + '"';
            return RegExp(t).test(v) ? v.replace(t, t + ' selected="selected"') : v;
        })
        .join('\n');
});

async function openModal(event, tileId) {
    let modal = document.getElementById("tileModal"); 
    modal.style.display = "block";
    modal.innerHTML = Handlebars.templates.TileForm({ "id": tileId }).replace(/[\u200B-\u200D\uFEFF]/g, "");
    let tile
    if (tileId !== undefined) {
        tile = await getTile(getUrlUserId(), tileId);
    }
    renderTileForm(event, tile);
}

function renderTileForm(event, tile) {
    let modal = document.getElementById("tileModal");
    modal.innerHTML = Handlebars.templates.TileForm(tile).replace(/[\u200B-\u200D\uFEFF]/g, "");
    // Change form when type is changed
    document.getElementById("type").onchange = (ev) => {
        tile = readTile(tile, false);
        renderAttributesForm(ev, readTile(tile, false));
    };
    // Submit button
    document.getElementById("submitModal").onclick = async (ev) => {
        tile = readTile(tile);
        let userId = getUrlUserId();
        let response;
        if (tile.id === undefined || tile.id === "") {
            response = await addTile(userId, tile);
        } else {
            response = await updateTile(userId, tile);
        }
        if (!response.ok) {
            document.getElementById("modalError").innerText = await response.text();
            return;
        } else {
            document.getElementById("modalError").innerText = "";
        }
        closeModal();
    }
    // Construct attributes form
    if (tile !== undefined && tile.type !== null) {
        renderAttributesForm(null, tile);
    }
}

function renderAttributesForm(event, tile) {
    if (tile.type === "paragraph") {
        setAttributesHtml(Handlebars.templates.ParagraphForm(tile.attributes))
    } else if (tile.type === "skills") {
        if (tile.attributes === undefined) tile.attributes = {};
        if (tile.attributes.skills === undefined) tile.attributes.skills = [{}];
        setAttributesHtml(Handlebars.templates.SkillsForm(tile.attributes))
        // register add button
        let addSkill = document.getElementById("addSkill");
        addSkill.onclick = (event) => {
            tile = readAttributes(tile);
            if (tile.attributes.skills.length <= 10) {
                tile.attributes.skills.push({});
            }
            renderAttributesForm(event, tile);
        }
        // register remove button
        let removeSkill = document.getElementById("removeSkill");
        removeSkill.onclick = (event) => {
            tile = readAttributes(tile);
            if (tile.attributes.skills.length > 1) {
                tile.attributes.skills.pop();
            }
            renderAttributesForm(event, tile);
        }
    }
}

function readTile(tile, includeAttributes = true) {
    if (tile === undefined) tile = {};
    tile.id = document.getElementById("tileId").value;
    tile.type = document.getElementById("type").value;
    tile.width = parseInt(document.getElementById("width").value);
    tile.height = parseInt(document.getElementById("height").value);
    if (includeAttributes) tile = readAttributes(tile);
    return tile;
}

function readAttributes(tile) {
    if (tile.attributes === undefined) tile.attributes = {};
    if (tile.type === "paragraph") {
        tile.attributes.title = document.getElementById("title").value;
        tile.attributes.paragraph = document.getElementById("paragraph").value;
    } else if (tile.type === "skills") {
        tile.attributes.title = document.getElementById("title").value;
        let skillIndex = 0;
        while (true) {
            let skillNameInput = document.getElementById(`name-${skillIndex}`);
            let skillPercentageInput = document.getElementById(`percentage-${skillIndex}`);
            if (skillNameInput === null) return tile;
            tile.attributes.skills[skillIndex] = {
                name: skillNameInput.value,
                percentage: parseInt(skillPercentageInput.value)
            }
            skillIndex++;
        }
    }
    return tile;
}

function setAttributesHtml(html) {
    document.getElementById("tileAttributes").innerHTML = html.replace(/[\u200B-\u200D\uFEFF]/g, "");
}

function closeModal() {
    document.getElementById("tileModal").style.display = "none";
}