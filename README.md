# Expense App

ASP.NET CoreとVue.jsの学習用に作成した、シンプルな家計簿アプリ。

## 機能

- 支出一覧の取得
- 支出の新規登録
- 支出の編集
- 支出の削除

## 使用技術

### Backend
- C#
- ASP.NET Core
- Entity Framework Core
- PostgreSQL

### Frontend
- Vue.js
- JavaScript
- Vite

## 構成

- `ExpenseApp` - ASP.NET Core / REST API
- `expense-frontend` - Vue.js

## 処理の流れ

Vue.jsからREST APIを呼び出し、ASP.NET Coreを通してPostgreSQLのデータを操作。

Vue.js → ASP.NET Core API → Service → Entity Framework Core → PostgreSQL