import { test, expect } from '@playwright/test';

test('Check for warning alert', async ({ page }) => {
    await page.goto('http://localhost:5170');

    await page.locator('input.mud-input-root.mud-input-root-filled').clear();
    await page.locator('input.mud-input-root.mud-input-root-filled').fill('0');

    await page.locator('button:has-text("Search drugs")').click();

    await expect(page.locator('.mud-alert-message')).toContainText("Limit value inputed as less than 1, Changed to 1");
});
