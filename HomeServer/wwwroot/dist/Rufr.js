import createClient from "../node_modules/openapi-fetch/src/index.js";
var Rufr;
(function (Rufr) {
    function GetClient() {
        return createClient({ baseUrl: "http://localhost:5167/" });
    }
    // export async function Get(path: string) : Promise<any> {
    //     try {
    //         const client = GetClient();
    //
    //         console.log("Rufing path: " + path);
    //         const { data, error } = await client.GET(path);
    //         console.log(data);
    //         console.log(error);
    //        
    //     } catch (error) {
    //         //console.error(error.message);
    //     }
    // }
    async function RufPost(formData) {
        const client = GetClient();
        const json = Object.fromEntries(formData.entries());
        console.table(Object.fromEntries(formData.entries()));
        const { data, error } = await client.POST("/api/register", { body: json });
        console.log(data);
        console.log(error);
    }
    Rufr.RufPost = RufPost;
    // export async function RufTest(path: string) : Promise<any> {
    //     const client = GetClient();
    //     return await client.GET(path, );
    // }
})(Rufr || (Rufr = {}));
export default Rufr;
//# sourceMappingURL=Rufr.js.map