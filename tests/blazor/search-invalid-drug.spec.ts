import { test, expect } from '@playwright/test';

test('Search for a invalid drug', async ({ page }) => {
    await page.goto('http://localhost:5170');

    await page.locator('label:has-text("Expected drug name")').waitFor({ state: 'visible' });
    await page.locator('label:has-text("Expected drug name")').clear();
    await page.locator('label:has-text("Expected drug name")').fill('InvalidDrugName');
    
    await page.locator('button:has-text("Search drugs")').click();

    await expect(page.locator('table')).toHaveCount(0);


    const errorMessage = page.locator('p[style="color: red;"]');
    await errorMessage.waitFor({ state: 'visible', timeout: 10000 });

    // Check if the error message contains the "404" text
    await expect(errorMessage).toHaveText(/404/);
});
