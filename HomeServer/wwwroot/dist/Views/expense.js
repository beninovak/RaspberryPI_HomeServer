export class ExpenseView {
    constructor() {
        this.name = "Expense";
    }
    async Load() {
        console.log("Loading expense view");
        const htmlResponse = await fetch("../../Views/expense.html");
        const html = await htmlResponse.text();
        const cssLink = document.createElement("link");
        cssLink.href = "../../Styles/expense.css";
        cssLink.rel = "stylesheet";
        document.querySelector("head").append(cssLink);
        document.querySelector("main").innerHTML = html;
    }
}
//# sourceMappingURL=expense.js.map