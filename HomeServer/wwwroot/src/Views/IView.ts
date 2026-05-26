export interface IView {
    readonly name: string;
    Load() : Promise<void>;
    // TODO - maybe dodej "type" property k je enum? Da se ne dela string comparisonov z loadanjem
}