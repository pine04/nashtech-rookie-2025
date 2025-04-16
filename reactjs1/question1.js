const getInfo = ({ firstName, lastName, age }) =>
    `${firstName} ${lastName}. Age: ${age}`;

const person1 = {
    firstName: "Son Tung",
    lastName: "MTP",
    age: 25,
};
// Output: Son Tung MTP. Age: 25
// Explanation: The getInfo function expects an object with properties `firstName`, `lastName`, and `age` to construct a string using a template literal.
// When person1 is passed into getInfo, the function substitutes the values inside person1 into the template literal, resulting in the output string.
console.log(getInfo(person1));
// Output: Huan Rose. Age: undefined
// Explanation: The argument passed into getInfo does not have the `age` property. Thus, the age is undefined.
console.log(getInfo({ firstName: "Huan", lastName: "Rose" }));
const person2 = {
    firstName: "Son Tung",
    lastName: "MTP",
    age: 25,
};
// Output: false
// Explanation: person1 and person2 are two different objects. They just have the same property names and values.
console.log(person1 === person2);

const setPersonName = (person, name) => {
    person.name = name;
};
setPersonName(person1, "Tung Nui");
// Output: Son Tung MTP. Age: 25
// Explanation: The properties `firstName`, `lastName`, and `age` of person1 are untouched by the setPersonName function.
// The setPersonName function only adds a new `name` property to the object.
// Because of this, getInfo returns the same string as before.
console.log(getInfo(person1));
// Output: Son Tung MTP. Age: 25
// Explanation: We create a new object with the same properties as person2 plus an additional `name` property.
// getInfo does not use the `name` property, and as such the output is the same as above.
console.log(getInfo({ ...person2, name: "Tung Nui" }));
