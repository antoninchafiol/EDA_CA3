import { test, expect } from '@playwright/test';

test('Loading state during API request', async ({ page }) => {
    await page.goto('https://localhost:7057/');

    await page.locator('button:has-text("Search drugs")').click();
    
    await expect(page.locator('text=Loading drugs')).toBeVisible();

    await page.locator('table').waitFor({ state: 'visible' });
    await expect(page.locator('text=Loading drugs')).not.toBeVisible();
});
