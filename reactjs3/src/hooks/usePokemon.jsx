import { useState, useEffect } from "react";

export default function usePokemon(id) {
    const [pokemon, setPokemon] = useState(null);
    const [status, setStatus] = useState("loading");

    useEffect(() => {
        async function getPokemon() {
            setStatus("loading");

            const res = await fetch(`https://pokeapi.co/api/v2/pokemon/${id}`);

            if (res.status !== 200) {
                setStatus("error");
                return;
            }

            const data = await res.json();

            setPokemon(data);
            setStatus("success");
        }

        getPokemon();
    }, [id]);

    return { pokemon, status };
}