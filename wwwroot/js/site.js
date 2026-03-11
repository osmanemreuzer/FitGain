// ============================================
// FitGain - Site JavaScript
// ============================================

document.addEventListener('DOMContentLoaded', function () {

    // --- Navbar scroll effect ---
    const navbar = document.getElementById('mainNavbar');
    if (navbar) {
        window.addEventListener('scroll', function () {
            if (window.scrollY > 30) {
                navbar.classList.add('scrolled');
            } else {
                navbar.classList.remove('scrolled');
            }
        });
    }

    // --- Fade-in-up animation on scroll (Intersection Observer) ---
    const animatedElements = document.querySelectorAll('.animate-on-scroll');
    if (animatedElements.length > 0) {
        const observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('animate-fade-in-up');
                    observer.unobserve(entry.target);
                }
            });
        }, {
            threshold: 0.1,
            rootMargin: '0px 0px -30px 0px'
        });

        animatedElements.forEach(function (el) {
            observer.observe(el);
        });
    }

    // --- Stagger animation for card lists ---
    const staggerContainers = document.querySelectorAll('.stagger-container');
    staggerContainers.forEach(function (container) {
        const children = container.querySelectorAll('.stagger-item');
        children.forEach(function (child, index) {
            child.style.animationDelay = (index * 0.1) + 's';
            child.classList.add('animate-fade-in-up');
        });
    });

});
