export async function LoadView(view: string) : Promise<void> {
    const main: HTMLElement | null = document.querySelector("main");

    if (main == null) {
        throw new DOMException("No <main> tag found. Something is very wrong!");
    }

    const response = await fetch(`Views/${view}`);
    const html = await response.text();

    if (!html.length) {
        main.innerHTML = `View {viewName} not implemented yet!`;
        return;
    }

    console.log(await response.text());
}

// async function LoadComponent(componentPath: string, parentSelector: string): Promise<void> {
//     const response = await fetch(componentPath);
//     const html = await response.text();
//
//     document.querySelector(parentSelector)!.innerHTML = html;
// }

export * as Loaders from "./loaders";