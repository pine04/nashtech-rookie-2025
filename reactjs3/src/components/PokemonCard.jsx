export default function PokemonCard({ id, name, weight, frontPicture, backPicture }) {
    return (
        <div>
            <p>ID: {id}</p>
            <p>Name: {name}</p>
            <p>Weight: {weight}</p>
            <img src={frontPicture} width={288} height={288} style={{ imageRendering: "pixelated" }} />
            <img src={backPicture} width={288} height={288} style={{ imageRendering: "pixelated" }} />
        </div>
    )
}