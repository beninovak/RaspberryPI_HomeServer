import { TodoView } from "./Views/todo.js";
import { ExpenseView } from "./Views/expense.js";
var Loaders;
(function (Loaders) {
    Loaders.Views = [
        new TodoView(),
        new ExpenseView(),
        //new ProfileView(),
    ];
    async function LoadView(view) {
        const main = document.querySelector("main");
        let viewToLoad = Loaders.Views.find(IView => IView.name == view.name);
        if (viewToLoad === undefined) {
            main.innerHTML = `View "${view.name}" not implemented yet!`;
            return;
        }
        await viewToLoad?.Load();
    }
    Loaders.LoadView = LoadView;
    // async function LoadComponent(componentPath: string, parentSelector: string): Promise<void> {
    //     const response = await fetch(componentPath);
    //     const html = await response.text();
    //
    //     document.querySelector(parentSelector)!.innerHTML = html;
    // }
    //export * as Loaders from "./loaders";    
})(Loaders || (Loaders = {}));
export default Loaders;
//# sourceMappingURL=loaders.js.map