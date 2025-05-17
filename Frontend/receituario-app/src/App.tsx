import React from 'react';
import './App.css';
import ReceituarioForm from './components/ReceituarioForm';

function App() {
    return (
        <div className="App">
            <header>
                <h1>Aplicativo de Verificação de Receituário</h1>
            </header>
            <main>
                <ReceituarioForm />
            </main>
        </div>
    );
}

export default App;