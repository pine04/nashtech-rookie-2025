import { useState } from "react";

import WelcomeScreen from "./screens/WelcomeScreen";
import CounterScreen from "./screens/CounterScreen";
import CheckboxesScreen from "./screens/CheckboxesScreen";
import PokemonScreen from "./screens/PokemonScreen";

function App() {
    const [screen, setScreen] = useState("welcome");

    return (
        <main>
            <select
                name="screen"
                value={screen}
                onChange={(e) => setScreen(e.target.value)}
            >
                <option value="welcome">Welcome</option>
                <option value="counter">Counter</option>
                <option value="checkboxes">Checkboxes</option>
                <option value="pokemon">Pokemon</option>
            </select>
            <p>Option selected: {screen}</p>

            {screen === "welcome" && <WelcomeScreen />}
            {screen === "counter" && <CounterScreen />}
            {screen === "checkboxes" && <CheckboxesScreen />}
            {screen === "pokemon" && <PokemonScreen />}
        </main>
    );
}

export default App;
