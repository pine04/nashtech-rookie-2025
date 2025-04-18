const people = [
    { name: "Hoangdd", age: 34, color: "red" },
    { name: "Son Tung MTP", age: 25, color: "yellow" },
    { name: "Ronaldo", age: 37, color: "green" },
];

export default function WelcomeScreen() {
    return (
        <div>
            {people.map((person, index) => (
                <WelcomeCard {...person} key={index} />
            ))}
        </div>
    );
}

function WelcomeCard({ name, age, color }) {
    return (
        <div style={{ backgroundColor: color }}>
            <h1>Hello {name}</h1>
            <h2>Age: {age}</h2>
        </div>
    );
}