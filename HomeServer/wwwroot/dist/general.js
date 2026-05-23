"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("./types/api");
const loaders_1 = require("./loaders");
window.onload = async () => {
    var buttons = document.querySelectorAll("nav button");
    buttons.forEach(btn => {
        btn.onclick = async () => {
            await loaders_1.Loaders.LoadView(btn.getAttribute("data-view"));
        };
    });
    //await LoadView("todo");
};
//# sourceMappingURL=general.js.map