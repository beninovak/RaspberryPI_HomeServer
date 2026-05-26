import { IView } from "./Views/IView.js";
import { TodoView } from "./Views/todo.js";
import { ExpenseView } from "./Views/expense.js";

namespace Loaders {
    export const Views: Array<IView> = [
        new TodoView(),
        new ExpenseView(),
        //new ProfileView(),
    ];
    
    export async function LoadView(view: IView) : Promise<void> {
        const main: HTMLElement = document.querySelector("main")!;

        let viewToLoad : IView | undefined = Views.find(IView => IView.name == view.name);
        if (viewToLoad === undefined) {
            main.innerHTML = `View "${view.name}" not implemented yet!`;
            return;
        }
        
        await viewToLoad?.Load();
    }

    // async function LoadComponent(componentPath: string, parentSelector: string): Promise<void> {
    //     const response = await fetch(componentPath);
    //     const html = await response.text();
    //
    //     document.querySelector(parentSelector)!.innerHTML = html;
    // }

    //export * as Loaders from "./loaders";    
}

export default Loaders;