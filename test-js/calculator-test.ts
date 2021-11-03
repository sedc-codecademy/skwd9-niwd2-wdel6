import { Calculator } from "./calculator";


// 1. Arrange
const addExpected = 3;
console.log(`Calling Calculator.add(1, 2). The Expected value is ${addExpected}`);
// 2. Act
const addResult = Calculator.add(1, 2);
// 3. Assert
console.log(addResult);
console.log(addExpected === addResult ? "\x1b[32mPassed\x1b[0m" : "\x1b[31mFailed\x1b[0m");


// 1. Arrange
const mulExpected = 4;
console.log(`Calling Calculator.multiply(2, 2). The Expected value is ${mulExpected}`);
// 2. Act
const mulResult = Calculator.multiply(2, 2);
// 3. Assert
console.log(mulResult);
console.log(mulExpected === mulResult ? "\x1b[32mPassed\x1b[0m" : "\x1b[31mFailed\x1b[0m");


// 1. Arrange
const mul2Expected = 12;
const mul2First = 4;
const mul2Second = 3;

console.log(`Calling Calculator.multiply(${mul2First}, ${mul2Second}). The Expected value is ${mul2Expected}`);
// 2. Act
const mul2Result = Calculator.multiply(mul2First, mul2Second);
// 3. Assert
console.log(mul2Result);
console.log(mul2Expected === mul2Result ? "\x1b[32mPassed\x1b[0m" : "\x1b[31mFailed\x1b[0m");



