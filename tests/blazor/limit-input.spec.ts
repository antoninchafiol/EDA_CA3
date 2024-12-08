import { test, expect } from '@playwright/test';

test('Search with empty drug name (random drugs)', async ({ page }) => {
    await page.goto('https://localhost:7057');

    // Leave the drug name field empty
    await page.locator('input[placeholder="Expected drug name"]').fill('');
    
    // Set a limit on the results
    await page.locator('input[placeholder="Limit the max number of results by"]').fill('5');
    
    // Click the search button
    await page.locator('button:has-text("Search drugs")').click();

    // Wait for results to be displayed in the table
    const rows = page.locator('table tr');
    await expect(rows).toHaveCount(5); // Ensure the number of results matches the limit
});
