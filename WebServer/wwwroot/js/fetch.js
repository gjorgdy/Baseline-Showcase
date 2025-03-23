const baseUri = "https://localhost:7052/";

window.tiles = {};

window.onload = async function () {
    const connection = new signalR.HubConnectionBuilder().withUrl(baseUri + "updates").build();
    connection.on("AddTile", fillGrid);
    connection.on("UpdateTile", fillGrid);
    connection.on("ReorderTiles", fillGrid);
    await connection.start()
        .then(() => connection.invoke("joinGroup", getUrlUserId(), ));
}

async function getProfileRequest(id) {
    try {
        const response = await fetch(baseUri + `users/${id}/profile`, {
            "credentials": "include",
        });
        if (!response.ok) {
            return {error: response.statusText};
        }
        const profile = await response.json();
        for (let i in profile.tiles) {
            const tile = profile.tiles[i];
            window.tiles[tile.id] = tile;
        }
        return profile;
    } catch (error) {
        console.error(error.message);
        return {};
    }
}

async function getTileRequest(id, tileId) {
    if (window.tiles.hasOwnProperty(tileId)) {
        return window.tiles[tileId];
    }
    try {
        const response = await fetch(baseUri + `users/${id}/profile/${tileId}`, {
            "credentials": "include",
        });
        if (!response.ok) {
            return {error: response.statusText};
        }
        return response.json();
    } catch (error) {
        console.error(error.message);
        return {};
    }
}

async function addTileRequest(id, tile) {
    const response = await fetch(baseUri + `users/${id}/profile`, {
        method: 'PUT',
        body: JSON.stringify(tile),
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include"
    });
    if (response.ok) {
        await fillGrid();
    }
    return response;
}

async function updateTileRequest(id, tile) {
    const response = await fetch( baseUri + `users/${id}/profile/${tile.id}`, {
        method: 'PATCH',
        body: JSON.stringify(tile),
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include"
    })
    if (response.ok) {
        // await fillGrid();
    }
    return response;
}

async function reorderTilesRequest(id, newOrder) {
    try {
        await fetch( baseUri + `users/${id}/profile`, {
            method: 'PATCH',
            body: JSON.stringify({
                order: newOrder,
            }),
            headers: {
                "Content-Type": "application/json",
            },
            credentials: "include"
        })
    } catch (e) {
        // Reload grid content
        await fillGrid()
    }
}