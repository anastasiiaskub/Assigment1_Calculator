namespace Calc202603;

public class Calculator
{
    
    private string _leftBrackets = "(";
    private string _rightBrackets = ")";
    private string _operators = "+-*/^";
    private string[] _finctions = new string[] { "sin", "cos", "max" };
    private string _separator = ",";

   

    public void doCalc(string _input, out double _result)
    {
        ArrayList _tokens = new ArrayList();
        ArrayList _postFix = new ArrayList();

        tokenize(_input, out _tokens);
        Console.WriteLine($"infix: {_tokens.AsString()}");

        _postFix = InfixToPostfix(_tokens);
        Console.WriteLine($"postfix: {_postFix.AsString()}");

        evaluatePrefix(_postFix, out _result);
        
    }

    private void tokenize(string _inputT, out ArrayList _resultT)
    {
        string _delims = _operators + _leftBrackets + _rightBrackets + _separator;
        string _buff = "";
        _resultT = new ArrayList();

        foreach (var _sym in _inputT)
        {
            if (_delims.Contains(_sym))
            {
                if (_buff.Length > 0)
                {
                    _resultT.Add(_buff);
                    _buff = "";
                }

                if (
                    _sym == '-'
                    &&
                    (
                        _resultT.Count() == 0
                        || 
                        (
                            _resultT.Count() > 0 
                            &&
                            _leftBrackets.Contains(_resultT.GetAt(_resultT.Count() - 1))
                        )
                    )
                )
                {
                    _buff += _sym;
                }
                else
                {
                    _resultT.Add(_sym.ToString());
                    _buff = "";
                }
            }
            else if (_sym != ' ')
            {
                _buff += _sym;
            }
            
        }

        if (_buff.Length > 0)
        {
            _resultT.Add(_buff);
        }
    }

    public int operatorWeight(string _operator)
    {
        if (_finctions.Contains(_operator))
        {
            return 4;
        }
        
        else if (_operator == "^")
        {
            return 3;
        }
        else if (_operator == "/" || _operator == "*")
        {
            return 2;
        }
        else if (_operator == "+" || _operator == "-" )
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public ArrayList InfixToPostfix(ArrayList _inFix)
    {
        Stack _stack = new Stack();
        ArrayList _postFix = new ArrayList();

        for (int _idxP = 0; _idxP < _inFix.Count(); _idxP++)
        {
            var _token = _inFix.GetAt(_idxP);
            // Console.WriteLine($"_inFix[{_idxP}]={_token}");
            if (_leftBrackets.Contains(_token))
            {
                _stack.Push(_token);
            }
            else if (_rightBrackets.Contains(_token))
            {
                while (_stack.Count() > 0 && !_leftBrackets.Contains(_stack.GetTop()))
                {
                    // Console.WriteLine($"{_stack.AsString()}");
                    _postFix.Add(_stack.Pull());
                }

                _stack.Pull();
                // Console.WriteLine($"{_stack.AsString()}");
            }
            else if (_operators.Contains(_token) || _finctions.Contains(_token))
            {
                while (operatorWeight(_stack.GetTop())>= operatorWeight(_token) )
                {
                    _postFix.Add(_stack.Pull());
                }
                
                _stack.Push(_token);
            }
            else if (_separator.Contains(_token))
            {
                
            }
            else
            {
                _postFix.Add(_token);
            }
        }
        // Console.WriteLine($"{_stack.AsString()}");
        while (_stack.Count() > 0)
        {
            if (_leftBrackets.Contains(_stack.GetTop()))
            {
                _stack.Pull();
                throw new Exception($"Missing closing bracket");
            }
            else
            {
                _postFix.Add(_stack.Pull());
            }
        }

        return _postFix;
    }

    public double getStackValue(Stack _stack)
    {
        string _tokenStack = _stack.Pull();
        double _num;

        if (!double.TryParse(_tokenStack, out _num))
        {
            throw new Exception($"token {_tokenStack} is not number");
        }

        return _num;
    }

    public void evaluatePrefix(ArrayList _postFix, out double _result)
    {
        _result = 0;
        Stack _stack = new Stack();
        string _token;
        // string _tokenStack1;
        // string _tokenStack2;
        double _num1;
        double _num2;
        double _res0;

        for (int _idxP = 0; _idxP < _postFix.Count(); _idxP++)
        {
            _token = _postFix.GetAt(_idxP);
            // Console.WriteLine($"_postFix[{_idxP}]={_token}");
            if (_finctions.Contains(_token))
            {
                _num1 = getStackValue(_stack);

                if (_token == "sin")
                {
                    _res0 = Math.Sin(_num1);
                    Console.WriteLine($"sin({_num1}) = {_res0}");
                }
                else if (_token == "cos")
                {
                    _res0 = Math.Cos(_num1);
                    Console.WriteLine($"cos({_num1}) = {_res0}");
                }
                else if (_token == "max")
                {
                    _num2 = getStackValue(_stack);
                    _res0 = Math.Max(_num1, _num2);
                    Console.WriteLine($"max({_num1}, {_num2}) = {_res0}");
                }
                else
                {
                    _res0 = 0;
                    throw new Exception($"function {_token} unknown");
                }

                _stack.Push(_res0.ToString());
            }

            else if (_operators.Contains(_token))
            {
                _num1 = getStackValue(_stack);
                _num2 = getStackValue(_stack);

                if (_token == "-")
                {
                    _res0 = _num2 - _num1;
                }
                else if (_token == "+")
                {
                    _res0 = _num2 + _num1;
                }
                else if (_token == "/")
                {
                    _res0 = _num2 / _num1;
                }
                else if (_token == "*")
                {
                    _res0 = _num2 * _num1;
                }
                else if (_token == "^")
                {
                    _res0 = Math.Pow(_num2, _num1);
                }
                else
                {
                    _res0 = 0;
                    throw new Exception($"Operator {_token} unknown");
                }
                _stack.Push(_res0.ToString());
            }
            else
            {
                if (!double.TryParse(_token, out _res0))
                {
                    throw new Exception($"function/operator {_token} unknown");
                }
                else
                {
                    _stack.Push(_res0.ToString());
                }
                
            }
            
        }

        _token = _stack.Pull();
        if (!double.TryParse(_token, out _result))
        {
            throw new Exception($"Token {_token} is not a number");
        }

    }
    
    
}


