import "./types/api";
import { Loaders } from "./loaders";

window.onload = async () => {
    var buttons: NodeListOf<HTMLButtonElement> = document.querySelectorAll("nav button");

    buttons.forEach(btn => {
        btn.onclick = async () => {
            await Loaders.LoadView(btn.getAttribute("data-view")!);
        }
    });
    
    
    //await LoadView("todo");
};