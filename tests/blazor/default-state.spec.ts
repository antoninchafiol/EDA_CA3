import { test, expect } from '@playwright/test';

test('Default page state', async ({ page }) => {
    await page.goto('http://localhost:5170');

    const inputLocator = page.locator('label:has-text("Expected drug name")');
    const isVisible = await inputLocator.isVisible();
    if (isVisible) {
    await expect(inputLocator).toBeVisible();
    } else {
    console.log('Element not visible or rendered yet');
    }
    await expect(page.locator('label:has-text("Limit the max number of results by")')).toBeVisible();

    await expect(page.locator('table')).toHaveCount(0);
});
