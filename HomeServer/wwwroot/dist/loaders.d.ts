import { IView } from "./Views/IView.js";
declare namespace Loaders {
    const Views: Array<IView>;
    function LoadView(view: IView): Promise<void>;
}
export default Loaders;
//# sourceMappingURL=loaders.d.ts.map