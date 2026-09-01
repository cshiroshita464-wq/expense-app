<script setup>
import { ref, onMounted } from 'vue'

// APIから取得した支出一覧を入れる
// refにすることで、値が変わると画面も自動で更新される
const expenses = ref([])

// 入力フォームの値
const date = ref('')
const amount = ref(0)
const category = ref('')
const memo = ref('')
const editingId = ref(null)

const resetForm = () => {
  date.value = ''
  amount.value = 0
  category.value = ''
  memo.value = ''
  editingId.value = null
}

// APIから支出一覧を取得する
const loadExpenses = async () => {
  const response = await fetch('http://localhost:5189/api/expenses')
  expenses.value = await response.json()
}

// 画面が表示されたときに支出一覧を取得する
onMounted(() => {
  loadExpenses()
})

// 入力した支出をAPIへ登録する
const createExpense = async () => {
  await fetch('http://localhost:5189/api/expenses', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      date: date.value,
      amount: Number(amount.value),
      category: category.value,
      memo: memo.value
    })
  })

  // 登録後、最新の支出一覧を取得して画面を更新する
  await loadExpenses()
  resetForm()
}

//編集用の値準備
const startEdit = (expense) => {
  editingId.value = expense.id

  date.value = expense.date
  amount.value = expense.amount
  category.value = expense.category
  memo.value = expense.memo
}

//編集
const updateExpense = async () => {
  await fetch(`http://localhost:5189/api/expenses/${editingId.value}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      date: date.value,
      amount: Number(amount.value),
      category: category.value,
      memo: memo.value
    })
  })

  await loadExpenses()
  resetForm()
}

const deleteExpense = async (id) => {
  await fetch(`http://localhost:5189/api/expenses/${id}`, {
    method: 'DELETE'
  })

  // 削除後に一覧を取り直す
  await loadExpenses()
}
</script>

<style scoped>
  .form {
    display: grid;
    grid-template-columns: 100px 300px;
    gap: 10px;
    width: 420px;
    margin-bottom: 30px;
  }

  .form label {
    display: flex;
    align-items: center;
  }

  .form input,
  .form button {
    padding: 8px;
  }

  .expense-list {
    width: 700px;
  }

  .expense-item {
    display: grid;
    grid-template-columns: 110px 100px 100px 1fr 60px 60px;
    gap: 8px;
    align-items: center;
    margin-bottom: 8px;
  }

  .expense-item span {
    padding: 6px;
  }

  .container {
    max-width: 800px;
    margin: 40px auto;
    padding: 0 20px;
  }

  h1 {
    margin-bottom: 30px;
  }

  h2 {
    margin-top: 30px;
    margin-bottom: 15px;
  }
</style>

<template>
  <main class="container">
    <h1>家計簿</h1>
    <div class="form">
      <label>日付</label>
      <input v-model="date" type="date" />
    
      <label>金額</label>
      <input v-model="amount" type="number" />
    
      <label>カテゴリ</label>
      <input v-model="category" type="text" />
    
      <label>メモ</label>
      <input v-model="memo" type="text" />
    
      <div></div>
      <button @click="editingId === null ? createExpense() : updateExpense()">
        {{ editingId === null ? '登録' : '更新' }}
      </button>
    </div>
    <h2>支出一覧</h2>
    <div class="expense-list">
      <div
        v-for="expense in expenses"
        :key="expense.id"
        class="expense-item"
      >
        <span>{{ expense.date }}</span>
        <span>{{ expense.category }}</span>
        <span>{{ expense.amount }}円</span>
        <span>{{ expense.memo }}</span>
    
        <button @click="startEdit(expense)">編集</button>
        <button @click="deleteExpense(expense.id)">削除</button>
      </div>
    </div>
  </main>
</template>