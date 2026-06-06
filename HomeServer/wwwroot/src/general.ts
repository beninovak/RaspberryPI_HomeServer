import "./types/api.js";
import { IView } from "./Views/IView.js";
import Loaders from "./loaders.js";
import Forms from "./forms.js";

let currentView: IView = Loaders.Views.values().next().value!;

window.onload = async () => {
    const nav = document.querySelector("nav")!;

    Loaders.Views.forEach(view => {
        let btn = document.createElement("button");
        btn.textContent = view.name;
        btn.setAttribute("data-view", view.name);

        btn.onclick = async () => {
            let btnViewName: string | null = btn.getAttribute("data-view");
            if (btnViewName == null) {
                throw new Error("Nav button has no data-view attribute");
            }

            let wantedView: IView = view;
            if (wantedView.name === currentView.name) {
                return;
            }
            
            await Loaders.LoadView(wantedView);
            currentView = wantedView;
        }

        nav.append(btn);
    });

    await Loaders.LoadView(currentView);
    await Forms.Init();  
}