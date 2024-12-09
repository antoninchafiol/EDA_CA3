import { test, expect } from '@playwright/test';

test('Search empty name and non-limited output', async ({ page }) => {
    await page.goto('http://localhost:5170');
   
    await page.locator('label:has-text("Expected drug name")').waitFor({ state: 'visible' });
    await page.locator('button:has-text("Search drugs")').click();

    await page.waitForSelector('table', { state: 'visible', timeout: 60000 });

    const pageInfo = await page.locator('.mud-table-page-number-information').textContent();
    const match = pageInfo.match(/of (\d+)/);
    const totalCount = match ? parseInt(match[1], 10) : null;

    await expect(totalCount).toBe(100); 
});
