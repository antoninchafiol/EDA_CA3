import { test, expect } from '@playwright/test';

test('Search for a drug', async ({ page }) => {
    await page.goto('https://localhost:7057');

    await page.locator('label:has-text("Expected drug name")').fill('Aspirin');
    
    await page.locator('label:has-text("Limit the max number of results by")').fill('1');

    await page.locator('button:has-text("Search drugs")').click();

    await expect(page.locator('table')).toHaveCount(1);
    await expect(page.locator('table tr')).toHaveText('Aspirin'); 
});
