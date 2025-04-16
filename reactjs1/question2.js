const students = [
    { name: "Alex", grade: 15, point: 15 },
    { name: "Devlin", grade: 15, point: 25 },
    { name: "Eagle", grade: 13, point: 12 },
    { name: "Sam", grade: 14, point: 26 },
];

// Sort that array by name, grade (Increase and Decrease) then log to console.
console.log(students.sort((a, b) => a.name.localeCompare(b.name)));
console.log(students.sort((a, b) => -a.name.localeCompare(b.name)));
console.log(students.sort((a, b) => a.grade - b.grade));
console.log(students.sort((a, b) => b.grade - a.grade));
console.log();

// Log all students whose points are greater than 15.
console.log(students.filter((student) => student.point > 15));
console.log();

// Calculate the total point of all students whose grades are equal 15
console.log(
    students.reduce(
        (total, student) =>
            student.grade === 15 ? total + student.point : total,
        0
    )
);
console.log();

// Remove the student’s name called “Sam” from the array then log to console.
console.log(students.filter((student) => student.name !== "Sam"));
