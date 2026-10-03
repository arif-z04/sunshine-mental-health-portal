# Database Transactions & ACID

## 1. What is a Transaction?
A **transaction** is a sequence of database operations treated as a single, indivisible unit of work. Either **ALL** operations succeed, or **NONE** of them do.

## 2. Real-World Analogy: An ATM Withdrawal
When you withdraw ৳5,000 from an ATM:
1. Deduct ৳5,000 from your account balance.
2. Dispense ৳5,000 cash from the machine.
If the power cuts out between step 1 and step 2, the transaction rolls back so you don't lose your money without receiving cash!
