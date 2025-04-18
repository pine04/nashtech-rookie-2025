import { useState } from "react";

import usePokemon from "../hooks/usePokemon";
import PokemonCard from "../components/PokemonCard";

export default function PokemonScreen() {
    const [currentId, setCurrentId] = useState(1);
    const { pokemon, status } = usePokemon(currentId);


    const handlePreviousId = () => {
        setCurrentId(prev => {
            if (prev > 1) return prev - 1;
            return prev;
        });
    }

    const handleNextId = () => setCurrentId(prev => prev + 1);

    return (
        <div>
            {
                status === "loading" && <div>Loading...</div>
            }
            {
                status === "error" && <div>An error happened. Cannot load pokemon.</div>
            }
            {
                status === "success" && pokemon &&
                <PokemonCard
                    id={pokemon.id}
                    name={pokemon.name}
                    weight={pokemon.weight}
                    frontPicture={pokemon.sprites.versions["generation-v"]["black-white"].front_default}
                    backPicture={pokemon.sprites.versions["generation-v"]["black-white"].back_default}
                />
            }
            <div style={{ marginTop: "1rem", display: "flex", gap: "1rem" }}>
                <button disabled={currentId === 1} onClick={handlePreviousId}>Previous</button>
                <button onClick={handleNextId}>Next</button>
            </div>
        </div>
    );
}