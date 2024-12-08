import { test, expect } from '@playwright/test';

test('Search with empty drug name', async ({ page }) => {
    await page.goto('https://localhost:7057');

    await page.locator('label:has-text("Expected drug name")').fill('');
    
    await page.locator('button:has-text("Search drugs")').click();

    const pageInfo = await page.locator('.mud-table-page-number-information').textContent();
    console.log('Page Info:', pageInfo);
    const match = pageInfo.match(/of (\d+)/);
    const totalCount = match ? parseInt(match[1], 10) : null;

    await expect(totalCount).toBe(100); 
});
