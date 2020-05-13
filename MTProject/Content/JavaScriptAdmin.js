const revealer = (actionBtn, revealBlock, config) => {
    const actionBtnEl = actionBtn;
    const revealBlockEl = revealBlock;
    const { onRevealEnd, initilalPosition } = config;
    const initialRedius = 10;

    let isMenuOpen = false;
    let reqId = null;
    let targetRadius = initialRedius;
    let elementRadius = targetRadius;

    const initRevealBlock = () => {
        revealBlock.style.clipPath = 'circle(var(--radius) at calc(100% - 50px) 45px)';
        revealBlockEl.style.setProperty('--radius', `${initialRedius}px`);
        revealBlockEl.setAttribute('data-active', true);
    };

    const getTargetRadius = inMenuOpen => {
        return inMenuOpen ? getMinimumRadius() : initialRedius;
    };

    const getMinimumRadius = () => {
        const { innerHeight, innerWidth } = window;

        return Math.sqrt(innerHeight ** 2 + innerWidth ** 2);
    }

    const animationStart = () => {
        elementRadius += (targetRadius - elementRadius) * 0.08;
        revealBlockEl.style.setProperty('--radius', `${elementRadius}px`);

        reqId = requestAnimationFrame(animationStart);

        // some bug with smal black point
        const isStopAnimation = isMenuOpen ? elementRadius > targetRadius : Math.round(elementRadius) === Math.round(targetRadius);
        if (isStopAnimation) {
            onRevealEnd();
            animationStop();
        }
    };

    const animationStop = () => {
        cancelAnimationFrame(reqId);
        reqId = null;
    };

    const onReveal = () => {
        isMenuOpen = !isMenuOpen;
        actionBtnEl.setAttribute('data-open', isMenuOpen);
        targetRadius = getTargetRadius(isMenuOpen);
        animationStart();
    };

    initRevealBlock();
    actionBtnEl.addEventListener('click', onReveal);
}

document.addEventListener('DOMContentLoaded', () => {
    const actionBtn = document.querySelector('.nav-btn-js');
    const revealBlock = document.querySelector('.nav-js');
    const config = {
        onRevealEnd() {
            console.log('end');
        },
    };

    revealer(actionBtn, revealBlock, config);
});
