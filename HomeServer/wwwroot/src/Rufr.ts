import type { paths, components } from "./types/api.js"
import createClient from "../node_modules/openapi-fetch/src/index.js";

namespace Rufr {
    
    function GetClient() {
        return createClient<paths>( { baseUrl: "http://localhost:5167/"});
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
    
    export async function RufPost(formData: FormData) : Promise<any> {
        const client = GetClient();
        const json = Object.fromEntries(formData.entries()) as components["schemas"]["RegisterDTO"];
        console.table(Object.fromEntries(formData.entries()))
        const { data, error } = await client.POST("/api/register", { body: json });

        console.log(data);
        console.log(error);
    }

    // export async function RufTest(path: string) : Promise<any> {
    //     const client = GetClient();
    //     return await client.GET(path, );
    // }
}

export default Rufr;