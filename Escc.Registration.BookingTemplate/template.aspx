<%@ Page Language="C#" %>
<asp:Content runat="server" ContentPlaceHolderID="metadata">
    {meta}
    <Metadata:MetadataControl runat="server" Title="{pagetitle}" DateCreated="2015-11-25" IpsvPreferredTerms="Registration of births; Registration of marriages; Registration of deaths" Creator="East Sussex County Council registration service" />
    <ClientDependency:Css runat="server" Files="ContentSmall" />
    <ClientDependency:Css runat="server" Files="ContentMedium" MediaConfiguration="Medium" />
    <ClientDependency:Css runat="server" Files="ContentLarge" MediaConfiguration="Large" />
    <link href='https://fonts.googleapis.com/css?family=Source+Sans+Pro:400,600' rel='stylesheet' type='text/css' />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="breadcrumb">
    <nav>
        <h2 class="aural">You are here</h2>
        <ol class="breadcrumb screen large">
            <li class="up level1"><span class="aural">Level 1: </span><a href="https://new.eastsussex.gov.uk/">Home</a></li>
            <li class="up level2"><span class="aural">Level 2: </span><a href="https://new.eastsussex.gov.uk/community/">Community</a></li>
            <li class="up level3"><span class="aural">Level 3: </span><a href="https://new.eastsussex.gov.uk/community/registration/">Births, deaths, marriages and civil partnerships</a></li>
            {breadcrumb}
        </ol>
    </nav><nav>
        <p class="screen small medium breadcrumb-mobile">You are in <a href="https://new.eastsussex.gov.uk/community/registration/">Births, deaths, marriages and civil partnerships</a></p>
    </nav>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="afterForm">
    <div class="full-page stopford">
        <article>
            <div class="content text-content">
                {content}
            </div>
        </article>
    </div>
</asp:Content>