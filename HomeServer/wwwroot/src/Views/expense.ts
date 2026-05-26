import { IView } from "./IView.js"

export class ExpenseView implements IView {
    readonly name: string = "Expense";

    async Load(): Promise<void> {
        console.log("Loading expense view");
        
        const htmlResponse = await fetch("../../Views/expense.html");
        const html = await htmlResponse.text();
        
        const cssLink = document.createElement("link");
        cssLink.href = "../../Styles/expense.css";
        cssLink.rel = "stylesheet";

        document.querySelector("head")!.append(cssLink);
        document.querySelector("main")!.innerHTML = html;
    }
}