import { useState } from "react";

const buttonStyle = {
    width: 32,
    height: 32,
    fontSize: 24,
    fontWeight: "bold",
};

const valueTextStyle = { fontSize: 24, fontWeight: "bold", margin: "0 1rem" };

export default function CounterScreen() {
    const [value, setValue] = useState(0);

    const handleDecrease = () => setValue((prev) => prev - 1);
    const handleIncrease = () => setValue((prev) => prev + 1);

    return (
        <div style={{ marginLeft: "2rem" }}>
            <button onClick={handleDecrease} style={buttonStyle}>
                -
            </button>
            <span style={valueTextStyle}>{value}</span>
            <button onClick={handleIncrease} style={buttonStyle}>
                +
            </button>
        </div>
    );
}