const toggle = document.getElementById("darkModeToggle");
const html = document.documentElement;

function updateIcon() {
    if (html.getAttribute("data-theme") === "dark") {
        toggle.classList.remove("bi-moon-stars-fill");
        toggle.classList.add("bi-sun-fill");
    } else {
        toggle.classList.remove("bi-sun-fill");
        toggle.classList.add("bi-moon-stars-fill");
    }
}

toggle.addEventListener("click", () => {
    const newTheme = html.getAttribute("data-theme") === "light" ? "dark" : "light";
    html.setAttribute("data-theme", newTheme);
    localStorage.setItem("theme", newTheme);
    updateIcon();
});

document.addEventListener("DOMContentLoaded", () => {
    const saved = localStorage.getItem("theme");
    if (saved) {
        html.setAttribute("data-theme", saved);
    }
    updateIcon();
});