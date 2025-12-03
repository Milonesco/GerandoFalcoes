document.addEventListener("DOMContentLoaded", function () {

    var navbar = document.getElementById("mainNavbar");
    if (navbar) {
        window.addEventListener("scroll", function () {
            if (window.scrollY > 50) {
                navbar.classList.add("scrolled-mode");
            } else {
                navbar.classList.remove("scrolled-mode");
            }
        });
    }

    // --- FUNÇÃO DE SMOOTH SCROLL PERSONALIZADA (GARANTIDA) ---
    function smoothScrollTo(targetPosition, duration = 800) {
        const startPosition = window.pageYOffset;
        const distance = targetPosition - startPosition;
        let startTime = null;

        function animation(currentTime) {
            if (startTime === null) startTime = currentTime;
            const timeElapsed = currentTime - startTime;
            const run = ease(timeElapsed, startPosition, distance, duration);
            window.scrollTo(0, run);
            if (timeElapsed < duration) requestAnimationFrame(animation);
        }

        // Função de easing para suavizar a animação (ease-in-out)
        function ease(t, b, c, d) {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t + b;
            t--;
            return -c / 2 * (t * (t - 2) - 1) + b;
        }

        requestAnimationFrame(animation);
    }

    // --- SMOOTH SCROLL PARA OS LINKS DA NAVBAR ---
    const navLinks = document.querySelectorAll('.navbar-nav .nav-link[href^="#"]');
    
    navLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            
            const targetId = this.getAttribute('href').substring(1);
            const targetSection = document.getElementById(targetId);
            
            if (targetSection) {
                const navbarHeight = navbar ? navbar.offsetHeight : 0;
                const additionalOffset = 30;
                const targetPosition = targetSection.offsetTop - navbarHeight - additionalOffset;
                
                // USA A FUNÇÃO PERSONALIZADA
                smoothScrollTo(targetPosition, 1000); // 1000ms = 1 segundo de duração
                
                // Fecha o menu mobile
                const navbarCollapse = document.querySelector('.navbar-collapse');
                if (navbarCollapse && navbarCollapse.classList.contains('show')) {
                    const bsCollapse = bootstrap.Collapse.getInstance(navbarCollapse) || new bootstrap.Collapse(navbarCollapse, { toggle: false });
                    bsCollapse.hide();
                }
            }
        });
    });

    // --- SMOOTH SCROLL PARA O LOGO (VOLTA AO TOPO) ---
    const brandLink = document.querySelector('.navbar-brand[href="#inicio"]');
    if (brandLink) {
        brandLink.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            
            // USA A FUNÇÃO PERSONALIZADA PARA VOLTAR AO TOPO
            smoothScrollTo(0, 1000);
            
            // Fecha menu mobile se aberto
            const navbarCollapse = document.querySelector('.navbar-collapse');
            if (navbarCollapse && navbarCollapse.classList.contains('show')) {
                const bsCollapse = bootstrap.Collapse.getInstance(navbarCollapse) || new bootstrap.Collapse(navbarCollapse, { toggle: false });
                bsCollapse.hide();
            }
        });
    }

    // --- LÓGICA DO DARK MODE ---
    const toggleBtn = document.getElementById('darkModeToggle');
    const htmlElement = document.documentElement;
    const icon = toggleBtn ? toggleBtn.querySelector('i') : null;

    const savedTheme = localStorage.getItem('theme') || 'light';
    htmlElement.setAttribute('data-theme', savedTheme);
    updateIcon(savedTheme);

    if (toggleBtn) {
        toggleBtn.addEventListener('click', () => {
            const currentTheme = htmlElement.getAttribute('data-theme');
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';

            htmlElement.setAttribute('data-theme', newTheme);
            localStorage.setItem('theme', newTheme);
            updateIcon(newTheme);
        });
    }

    function updateIcon(theme) {
        if (!icon) return;

        if (theme === 'dark') {
            icon.classList.remove('bi-moon-fill');
            icon.classList.add('bi-sun-fill');
            icon.style.color = "#ffd700";
        } else {
            icon.classList.remove('bi-sun-fill');
            icon.classList.add('bi-moon-fill');
            icon.style.color = "";
        }
    }

    // --- AUTO-HOVER NOS CÍRCULOS DE LOGOS DO CARROSSEL ---
    const marqueeContainer = document.querySelector('.marquee-container');
    
    if (marqueeContainer) {
        const logoCircles = marqueeContainer.querySelectorAll('.logo-circle');
        let mouseX = null;
        let mouseY = null;

        marqueeContainer.addEventListener('mousemove', function (e) {
            const rect = marqueeContainer.getBoundingClientRect();
            mouseX = e.clientX;
            mouseY = e.clientY - rect.top;
        });

        marqueeContainer.addEventListener('mouseleave', function () {
            mouseX = null;
            mouseY = null;
            logoCircles.forEach(circle => circle.classList.remove('auto-hovered'));
        });

        function checkCirclesUnderMouse() {
            if (mouseX === null || mouseY === null) return;

            logoCircles.forEach(circle => {
                const rect = circle.getBoundingClientRect();
                
                const isUnderMouse = (
                    mouseX >= rect.left &&
                    mouseX <= rect.right &&
                    mouseY >= 0 &&
                    mouseY <= marqueeContainer.clientHeight
                );

                if (isUnderMouse) {
                    circle.classList.add('auto-hovered');
                } else {
                    circle.classList.remove('auto-hovered');
                }
            });
        }

        function animateLogos() {
            checkCirclesUnderMouse();
            requestAnimationFrame(animateLogos);
        }

        animateLogos();
    }
});