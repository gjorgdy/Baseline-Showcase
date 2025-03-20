const baseUri = "https://localhost:7052/";

async function getProfile(id) {
    try {
        const response = await fetch(baseUri + `users/${id}/profile`, {
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

async function reorderTiles(id, newOrder) {
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