import { IView } from "./IView.js"

export class TodoView implements IView {
    readonly name: string = "Todo";

    async Load(): Promise<void> {
        console.log("Loading todo view");
        
        const htmlResponse = await fetch("../../Views/todo.html");
        const html = await htmlResponse.text();
        
        const cssLink = document.createElement("link");
        cssLink.href = "../../Styles/todo.css";
        cssLink.rel = "stylesheet";

        document.querySelector("head")!.append(cssLink);
        document.querySelector("main")!.innerHTML = html;
    }
}