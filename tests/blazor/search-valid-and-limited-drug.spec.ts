import { test, expect } from '@playwright/test';

test('Search for valid drug and for multiple output', async ({ page }) => {
    await page.goto('http://localhost:5170');

    await page.locator('label:has-text("Expected drug name")').waitFor({ state: 'visible' });
    await page.locator('label:has-text("Expected drug name")').clear();
    await page.locator('label:has-text("Expected drug name")').fill('ASPIRIN');
    
    await page.locator('input.mud-input-root.mud-input-root-filled').clear();
    await page.locator('input.mud-input-root.mud-input-root-filled').fill('5');

    await page.locator('button:has-text("Search drugs")').click();

    await page.waitForSelector('table', { state: 'visible', timeout: 60000 });
    var pageInfo = await page.locator('.mud-table-page-number-information').textContent();
    const match = pageInfo.match(/of (\d+)/);
    const totalCount = match ? parseInt(match[1], 10) : null;
    await expect(totalCount).toBe(5);
    
    const genericNames = await page.locator('table tbody tr td[data-label="Generic Name"]').allTextContents();
    for (const name of genericNames) {
        expect(name.toLowerCase()).toContain('aspirin');
    }
});
