import Rufr from "./Rufr.js";
var Forms;
(function (Forms) {
    async function Init() {
        document.querySelectorAll("form").forEach(form => {
            form.addEventListener("submit", (e) => {
                console.log("ON SUBMIT");
                e.preventDefault();
                debugger;
            });
        });
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
        });
    }
    Forms.Init = Init;
    window.onload = async () => {
        await Init();
    };
})(Forms || (Forms = {}));
export default Forms;
//# sourceMappingURL=forms.js.map