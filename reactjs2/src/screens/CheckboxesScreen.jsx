import { useState } from "react";

export default function CheckboxesScreen() {
    const [options, setOptions] = useState({
        coding: false,
        music: false,
        reading: false,
    });

    const handleCheck = (e) => {
        const checked = e.target.checked;
        const name = e.target.name;

        if (name === "all") {
            setOptions({
                coding: checked,
                music: checked,
                reading: checked,
            });
            return;
        }

        setOptions((prev) => ({
            ...prev,
            [name]: !prev[name],
        }));
    };

    return (
        <div style={{ marginLeft: "2rem" }}>
            <p>Choose your interests</p>

            <CheckboxOption
                label="All"
                name="all"
                checked={options.coding && options.music && options.reading}
                handleCheck={handleCheck}
            />
            <CheckboxOption
                label="Coding"
                name="coding"
                checked={options.coding}
                handleCheck={handleCheck}
            />
            <CheckboxOption
                label="Music"
                name="music"
                checked={options.music}
                handleCheck={handleCheck}
            />
            <CheckboxOption
                label="Reading books"
                name="reading"
                checked={options.reading}
                handleCheck={handleCheck}
            />

            <p>{JSON.stringify(options)}</p>
        </div>
    );
}

function CheckboxOption({ label, name, checked, handleCheck }) {
    return (
        <div>
            <label>
                <input
                    type="checkbox"
                    name={name}
                    checked={checked}
                    onChange={handleCheck}
                />
                {label}
            </label>
        </div>
    );
}
