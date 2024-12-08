import { test, expect } from '@playwright/test';

test('Search for a drug', async ({ page }) => {
    await page.goto('http://localhost:5170');

    await page.locator('label:has-text("Expected drug name")').waitFor({ state: 'visible' });
    await page.locator('label:has-text("Expected drug name")').clear();
    await page.locator('label:has-text("Expected drug name")').fill('InvalidDrugName');
    
    await page.locator('input.mud-input-root.mud-input-root-filled').clear();
    await page.locator('input.mud-input-root.mud-input-root-filled').fill('1');

    await page.locator('button:has-text("Search drugs")').click();

    await expect(page.locator('table')).toHaveCount(0);
    await expect(page.locator('p[style="color: red;"]')).toHaveText('404');
});
