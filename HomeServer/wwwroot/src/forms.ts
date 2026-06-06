import Rufr from "./Rufr.js";

namespace Forms {
   export async function Init() : Promise<void> {
       document.querySelectorAll("form").forEach(form => {
           form.addEventListener("submit", (e) => {
               console.log("ON SUBMIT");
               e.preventDefault();
               debugger;
           })
       })
       
       document.querySelectorAll("form button.submit").forEach(submitBtn => {
           submitBtn.addEventListener("click", async () => {
               const form = submitBtn.closest("form");
               if (form === null) {
                   return;
               }
               const response = await Rufr.RufPost(new FormData(form));
               
               console.log(response);
               
               //form.submit();
               //e.preventDefault();
           });
       })
   }
   
    window.onload = async () => {
        await Init();
    }
}

export default Forms;