import { test, expect } from '@playwright/test';

test('Loading state during API request', async ({ page }) => {
    await page.goto('http://localhost:5170/');

    await page.locator('button:has-text("Search drugs")').click();
    
    await expect(page.locator('text=Loading drugs')).toBeVisible();

    await page.waitForSelector('table', { state: 'visible', timeout: 60000 });

    await expect(page.locator('text=Loading drugs')).not.toBeVisible();
});
