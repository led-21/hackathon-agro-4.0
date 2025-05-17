import React, { useState } from 'react';
import axios from 'axios';
import ReactMarkdown from 'react-markdown';
import remarkGfm from 'remark-gfm'; // Importando plugin para suportar tabelas no Markdown

const ReceituarioForm: React.FC = () => {
    const [receituario, setReceituario] = useState('');
    const [markdown, setMarkdown] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');
        setMarkdown('');
        setLoading(true);
      
        try {
          const response = await axios.post<String>(
            `http://localhost:5260/verificar/receituario?receituario=${encodeURIComponent(receituario)}`,
            {},
            { headers: { 'Content-Type': 'application/json' } }
          );
          setMarkdown(response.data as unknown as string);
        } catch (err) {
          console.error('Erro na requisição:', err); 
          if (axios.isAxiosError(err)) {
            setError(
              `Erro: ${err.response?.status} - ${
                err.response?.data?.message || err.message
              }`
            );
          } else {
            setError('Erro desconhecido. Verifique a conexão ou o formato do receituário.');
          }
        } finally {
          setLoading(false);
        }
      };

    return (
        <div className="container">
            <h2>Verificar Receituário</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="receituario">Receituário:</label>
                    <textarea
                        id="receituario"
                        value={receituario}
                        onChange={(e) => setReceituario(e.target.value)}
                        placeholder="Digite o receituário"
                        required
                        rows={10} // Define uma altura maior para o campo de entrada
                        style={{ width: '100%', resize: 'vertical' }} // Permite ajuste de tamanho
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? 'Verificando...' : 'Verificar'}
                </button>
            </form>
            {error && <p className="error">{error}</p>}
            {markdown && (
                <div className="markdown">
                    <h3>Resultado:</h3>
                    <ReactMarkdown remarkPlugins={[remarkGfm]}>{markdown}</ReactMarkdown>
                </div>
            )}
        </div>
    );
};

export default ReceituarioForm;
