
document.querySelector("#find").addEventListener("click", async function () {
    var date1 = document.getElementById("start").value;
    var date2 = document.getElementById("finish").value;

    if (date1 != "" && date2 != "") {
        window.location.href = `/api/application/items/${date1}/${date2}`;
    }
});

var check = true;
window.onload = async function () {

    if (window.sessionStorage.getItem("check") == null) {
        const getCount = await fetch(`/api/application/itemsCount`, {
            method: "GET",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
        });

        var allCount = await getCount.text();
        window.sessionStorage.setItem("count", allCount);

        document.querySelector(".count-application").innerHTML += allCount;
    }
    else {
        document.querySelector(".count-application").innerHTML += window.sessionStorage.getItem("count");
    }

    window.sessionStorage.setItem("check", check);

    var num = document.querySelectorAll(".count-application")[1].textContent.split(":")[1];
    var num2 = document.querySelectorAll(".count-application")[0].textContent.split(":")[1];

    if (Number(num) > Number(num2)) {
        var str = document.querySelector(".count-application").innerHTML.split(":")[0];
        document.querySelector(".count-application").innerHTML = `${str}: ${num}`;
        window.sessionStorage.setItem("count", num);
    }
};
