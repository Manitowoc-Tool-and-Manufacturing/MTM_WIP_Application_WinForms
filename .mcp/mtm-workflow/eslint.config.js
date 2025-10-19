import js from "@eslint/js";
import tseslint from "typescript-eslint";

export default tseslint.config(
  js.configs.recommended,
  ...tseslint.configs.recommended,
  {
    files: ["**/*.ts"],
    ignores: ["dist/"],
    languageOptions: {
      parserOptions: {
        ecmaVersion: 2022,
        sourceType: "module"
      }
    },
    rules: {
      "@typescript-eslint/no-unused-vars": "off",
      "@typescript-eslint/no-explicit-any": "off",
  "@typescript-eslint/no-unused-expressions": "off",
      "no-unused-vars": "off",
      "no-unused-expressions": "off",
      "prefer-const": "warn"
    }
  }
);
