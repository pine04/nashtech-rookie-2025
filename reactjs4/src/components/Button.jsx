export default function Button({ type, label, disabled, onClick }) {
    return (
        <button type={type} disabled={disabled} onClick={onClick} className="bg-blue-600 text-white px-3 py-2 rounded-md disabled:bg-gray-400 hover:bg-blue-400 transition-colors">
            {label}
        </button>
    );
}