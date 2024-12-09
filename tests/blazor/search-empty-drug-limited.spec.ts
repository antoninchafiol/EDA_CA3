import { test, expect } from '@playwright/test';

test('Search limited output', async ({ page }) => {
    await page.goto('http://localhost:5170');

    await page.locator('input.mud-input-root.mud-input-root-filled').clear();
    await page.locator('input.mud-input-root.mud-input-root-filled').fill('5');

    await page.locator('button:has-text("Search drugs")').click();

    await page.waitForSelector('table', { state: 'visible', timeout: 60000 });

    const pageInfo = await page.locator('.mud-table-page-number-information').textContent();
    console.log('Page Info:', pageInfo);
    const match = pageInfo.match(/of (\d+)/);
    const totalCount = match ? parseInt(match[1], 10) : null;

    await expect(totalCount).toBe(5); 
});
