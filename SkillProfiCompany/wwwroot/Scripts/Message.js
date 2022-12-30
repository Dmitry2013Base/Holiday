var applicationId = document.querySelector("#head").textContent.split(" ")[1].substring(1);
var select = document.querySelector("select");

select.addEventListener('change', async function (e) {

    await fetch(`/api/application/items`, {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                applicationId: applicationId,
                newStatusId: e.target.value,
            },
        )
    });
})

window.onload = async function () {
    var str = document.querySelector("h2").textContent.split(" ");
    var name = str[str.length - 1];
    name = name.substring(0, name.length - 1).substring(1);

    const getApplications = await fetch(`/api/application/items/${name.substring(1)}`, {
        method: "GET",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });

    var Applications = await getApplications.json();


    var list = document.querySelector(".user-list-message");

    Applications.forEach(function (i) {
        if (applicationId != i.id) {
            list.innerHTML +=
                `<li class="message-item">
                        <a href="/api/application/messages/${i.id}">${i.userMessage}</a>
                    </li>`
        }
    });

    const getStatuses = await fetch(`/api/application/items/statuses`, {
        method: "GET",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
    });

    var statuses = await getStatuses.json();
    var currentStatus = document.querySelector(".status").textContent;
    var num = 1;

    statuses.forEach(function (i) {

        if (currentStatus != i) {

            select.innerHTML += `<option value="${num}">${i}</option>`

        }
        else {
            select.innerHTML += `<option value="${num}" selected>${i}</option>`

        }
        num += 1;
    });
};