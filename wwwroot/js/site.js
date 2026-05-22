// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Hero Carousel
let currentSlide = 0;
let carouselInterval;

function initCarousel() {
    const carousel = document.querySelector('.hero-carousel');
    if (!carousel) return;

    const slides = carousel.querySelectorAll('.carousel-item');
    const indicators = carousel.querySelectorAll('.carousel-indicator');

    if (slides.length === 0) return;

    function showSlide(index) {
        slides.forEach(slide => slide.classList.remove('active'));
        indicators.forEach(indicator => indicator.classList.remove('active'));

        currentSlide = (index + slides.length) % slides.length;
        slides[currentSlide].classList.add('active');
        indicators[currentSlide].classList.add('active');
    }

    function nextSlide() {
        showSlide(currentSlide + 1);
    }

    // Auto play
    carouselInterval = setInterval(nextSlide, 5000);

    // Indicator click
    indicators.forEach((indicator, index) => {
        indicator.addEventListener('click', () => {
            clearInterval(carouselInterval);
            showSlide(index);
            carouselInterval = setInterval(nextSlide, 5000);
        });
    });

    // Pause on hover
    carousel.addEventListener('mouseenter', () => clearInterval(carouselInterval));
    carousel.addEventListener('mouseleave', () => {
        carouselInterval = setInterval(nextSlide, 5000);
    });
}

// Mega Menu
function initMegaMenu() {
    const menuTrigger = document.querySelector('[data-mega-menu-trigger]');
    const megaMenu = document.querySelector('.mega-menu');

    if (!menuTrigger || !megaMenu) return;

    let timeout;

    menuTrigger.addEventListener('mouseenter', () => {
        clearTimeout(timeout);
        megaMenu.classList.add('active');
    });

    menuTrigger.addEventListener('mouseleave', () => {
        timeout = setTimeout(() => {
            megaMenu.classList.remove('active');
        }, 300);
    });

    megaMenu.addEventListener('mouseenter', () => {
        clearTimeout(timeout);
    });

    megaMenu.addEventListener('mouseleave', () => {
        megaMenu.classList.remove('active');
    });
}

// Mobile Menu
function initMobileMenu() {
    const menuBtn = document.querySelector('[data-mobile-menu-btn]');
    const closeBtn = document.querySelector('[data-mobile-menu-close]');
    const mobileMenu = document.querySelector('.mobile-menu');
    const overlay = document.querySelector('.mobile-menu-overlay');

    if (!menuBtn || !mobileMenu || !overlay) return;

    function openMenu() {
        mobileMenu.classList.add('active');
        overlay.classList.add('active');
        document.body.style.overflow = 'hidden';
    }

    function closeMenu() {
        mobileMenu.classList.remove('active');
        overlay.classList.remove('active');
        document.body.style.overflow = '';
    }

    menuBtn.addEventListener('click', openMenu);
    if (closeBtn) closeBtn.addEventListener('click', closeMenu);
    overlay.addEventListener('click', closeMenu);
}

// Scroll to Top Button
function initScrollToTop() {
    const scrollBtn = document.querySelector('.scroll-to-top');
    if (!scrollBtn) return;

    window.addEventListener('scroll', () => {
        if (window.pageYOffset > 300) {
            scrollBtn.classList.add('visible');
        } else {
            scrollBtn.classList.remove('visible');
        }
    });

    scrollBtn.addEventListener('click', () => {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    });
}

// Flash Sale Countdown Timer
function initCountdown() {
    const timerElements = document.querySelectorAll('[data-countdown]');
    
    timerElements.forEach(element => {
        const endTime = new Date(element.dataset.countdown).getTime();
        
        function updateTimer() {
            const now = new Date().getTime();
            const distance = endTime - now;

            if (distance < 0) {
                element.innerHTML = '<span class="text-red-500 font-semibold">Đã kết thúc</span>';
                return;
            }

            const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            const seconds = Math.floor((distance % (1000 * 60)) / 1000);

            const hoursEl = element.querySelector('[data-hours]');
            const minutesEl = element.querySelector('[data-minutes]');
            const secondsEl = element.querySelector('[data-seconds]');

            if (hoursEl) hoursEl.textContent = String(hours).padStart(2, '0');
            if (minutesEl) minutesEl.textContent = String(minutes).padStart(2, '0');
            if (secondsEl) secondsEl.textContent = String(seconds).padStart(2, '0');
        }

        updateTimer();
        setInterval(updateTimer, 1000);
    });
}

// Animate on Scroll
function initScrollAnimations() {
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-fade-in-up');
                observer.unobserve(entry.target);
            }
        });
    }, observerOptions);

    document.querySelectorAll('[data-animate]').forEach(el => {
        observer.observe(el);
    });
}

// Add to Cart Animation
function initAddToCart() {
    const addToCartBtns = document.querySelectorAll('[data-add-to-cart]');
    
    addToCartBtns.forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.preventDefault();
            
            // Add animation class
            this.classList.add('scale-95');
            setTimeout(() => {
                this.classList.remove('scale-95');
            }, 200);

            // Show notification (you can customize this)
            showNotification('Đã thêm vào giỏ hàng!');
        });
    });
}

function showNotification(message) {
    const notification = document.createElement('div');
    notification.className = 'fixed top-20 right-6 bg-green-500 text-white px-6 py-3 rounded-lg shadow-lg z-50 animate-slide-in';
    notification.textContent = message;
    
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.style.opacity = '0';
        notification.style.transform = 'translateX(100%)';
        setTimeout(() => notification.remove(), 300);
    }, 3000);
}

// Category Cards Animation
function initCategoryCards() {
    const categoryCards = document.querySelectorAll('.category-card-modern');

    if (categoryCards.length === 0) return;

    // Add stagger animation on page load
    categoryCards.forEach((card, index) => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(30px)';

        setTimeout(() => {
            card.style.transition = 'all 0.6s cubic-bezier(0.4, 0, 0.2, 1)';
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }, 100 * index);
    });

    // Add parallax effect on scroll
    window.addEventListener('scroll', () => {
        categoryCards.forEach(card => {
            const rect = card.getBoundingClientRect();
            const scrollPercent = (window.innerHeight - rect.top) / window.innerHeight;

            if (scrollPercent > 0 && scrollPercent < 1) {
                const image = card.querySelector('.category-image');
                if (image) {
                    image.style.transform = `scale(1.1) translateY(${scrollPercent * -20}px)`;
                }
            }
        });
    });
}

function initAccountMenus() {
    const accountMenus = document.querySelectorAll('[data-account-menu]');

    if (accountMenus.length === 0) return;

    function closeAll(exceptMenu) {
        accountMenus.forEach(menu => {
            if (menu !== exceptMenu) {
                menu.classList.remove('is-open');
                menu.querySelector('[data-account-menu-trigger]')?.setAttribute('aria-expanded', 'false');
            }
        });
    }

    accountMenus.forEach(menu => {
        const trigger = menu.querySelector('[data-account-menu-trigger]');

        if (!trigger) return;

        trigger.addEventListener('click', event => {
            event.stopPropagation();
            const isOpen = menu.classList.toggle('is-open');
            trigger.setAttribute('aria-expanded', String(isOpen));
            closeAll(menu);
        });
    });

    document.addEventListener('click', () => closeAll());
    document.addEventListener('keydown', event => {
        if (event.key === 'Escape') {
            closeAll();
        }
    });
}

// Initialize all functions when DOM is ready
document.addEventListener('DOMContentLoaded', function() {
    initCarousel();
    initMegaMenu();
    initMobileMenu();
    initScrollToTop();
    initCountdown();
    initScrollAnimations();
    initAddToCart();
    initCategoryCards();
    initAccountMenus();
});
