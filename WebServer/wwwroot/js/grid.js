// window.addEventListener("resize", setupGrid);
window.addEventListener("DOMContentLoaded", setupGrid);

function setupGrid() {
    const cellSize = 200; // Example of cell size in pixels
    const container = document.getElementById("container")
    let columnCount = Math.floor(container.offsetWidth / cellSize);
    columnCount = columnCount - (columnCount % 3);
    container.style.setProperty('--column-count', columnCount);

    const grid = document.getElementById("grid")
    new Sortable(grid, {
        // swap: true,
        // swapClass: '.highlight',
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
            let tileOrder = []

            console.log(evt.to);
            
            for (let key in evt.to.children) {
                let tile = evt.to.children[key];
                if (tile.id != null && tile.id !== "") {
                    tileOrder.push(tile.id);
                    console.log(tile.id);
                }
            }
            
            await fetch("https://localhost:7052/users/2/profile", {
                method: 'PATCH',
                body: JSON.stringify({
                    order: tileOrder,
                }),
                headers: {
                    "Content-Type": "application/json",
                },
                credentials: "include"
            })
            .then((response) => response.json())
            .then((json) => console.log(json));
        }
    });
}