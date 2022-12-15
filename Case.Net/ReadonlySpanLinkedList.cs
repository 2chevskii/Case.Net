namespace Case.Net;

public delegate void RsllAction(ReadonlySpanLinkedListNode node);

public delegate void RsllAction2(ReadonlySpanLinkedListNode node, int index);

public delegate void RsllAction3(
    ReadonlySpanLinkedListNode node,
    int index,
    ReadonlySpanLinkedList list
);

public unsafe ref struct ReadonlySpanLinkedList
{
    public ReadonlySpanLinkedListNode* Head;
    public int                         Count;
    public int                         TotalLength;

    public ReadonlySpanLinkedList(ReadonlySpanLinkedListNode head)
    {
        Head = &head;
        Count = 1;
        TotalLength = head.Value.Length;
    }

    public void Add(ReadOnlySpan<char> value)
    {
        var node = new ReadonlySpanLinkedListNode( this, value );

        Add( node );
    }

    public bool IsEmpty() => Head == null;

    public void Walk(RsllAction3 action)
    {
        ReadonlySpanLinkedListNode* current = Head;
        int                         index   = 0;

        do
        {
            action( *current, index, this );
            current = current->Next;
            index++;
        } while ( current != null );
    }

    public void Walk(RsllAction2 action)
    {
        ReadonlySpanLinkedListNode* current = Head;
        int                         index   = 0;

        do
        {
            action( *current, index );
            current = current->Next;
            index++;
        } while ( current != null );
    }

    public void Walk(RsllAction action)
    {
        ReadonlySpanLinkedListNode* current = Head;
        int                         index   = 0;

        do
        {
            action( *current );
            current = current->Next;
            index++;
        } while ( current != null );
    }

    private void Add(ReadonlySpanLinkedListNode node)
    {
        if ( Head == null )
        {
            Head = &node;
            Count = 1;
            TotalLength = Head->Value.Length;
        }
        else
        {
            ReadonlySpanLinkedListNode* current = Head;

            while ( current->HasNext() )
            {
                current = current->Next;
            }

            current->SetNext( node );

            Count++;
            TotalLength += node.Value.Length;
        }
    }
}

public unsafe ref struct ReadonlySpanLinkedListNode
{
    public readonly ReadonlySpanLinkedList*     List;
    public readonly ReadOnlySpan<char>          Value;
    public          ReadonlySpanLinkedListNode* Prev;
    public          ReadonlySpanLinkedListNode* Next;

    public ReadonlySpanLinkedListNode(ReadonlySpanLinkedList list, ReadOnlySpan<char> value)
    {
        List = &list;
        Value = value;
        Prev = null;
        Next = null;
    }

    public void SetNext(ReadonlySpanLinkedListNode node) { Next = &node; }

    public bool IsHead() => Prev == null;

    public bool HasNext() => Next != null;
}
